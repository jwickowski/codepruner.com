---
author: Jerzy Wickowski
categories:
  - ddd
date: 2024-07-26T05:40:58.000Z
draft: false
images:
  - images/posts/2024/2024-07-30-how-to-implement-an-invitation-for-a-user-that-does-not-exist.jpg
tags:
  - ddd
  - implementation
  - dotNet
  - ideas
title: How to implement an invitation for a user that does not exist
type: trending

---
I am creating an [application](https://www.vatahapp.com/) to improve the cooperation between trainers, players and sport enthusiasts. On one of the step I ran into a problem of inviting not existing people, at least in the system. Let allow to me to walk you thought the problems and its solution.

## Understand the process
Before we go deeper with the technical problems and solution I will describe the process of invitation. 
There are 3 options of configure join policy:
- `Open` - Anyone can join
- `OpenForRequest` - Anyone can send join request. The someone from the club can approve or reject it
- `InvitationOnly` - To join user should be invited and he should approve invitation

It is rather simple, but with every JoinPolicy user can be invited to the club he should approve the invitation. But there is also one interesting scenario to allow for joining
When user is invited and send a join request, then we can assume than it can became a member.

In the most of the situations we know that user has account in the system, because he has to have it to approve the invitation or send a join request, but there is a possibility that user will be invited to the group without have an account. Let image that it can be done with the usage of email or phone number.

So we have to have a relation between email/phone and user to solve the membership.

I had some ideas to solve it like:
- Creating a user account on `invitation`, but:
  - I cannot be use if the create the account in the future
  - I decided to use external library for account (AspNet Identity), so I am not able to create account
- On approving the request I can search invitations by email, but it is not clear and it can have bad performance in the future

So let check current solution and then we will go further to solve the issue.

## Current solution
In the 1st version of the implementation I didn't think about that case. So I just support userId. Let see the Entity:
``` csharp 
public class ClubMembershipEntity() : BaseEntity<ClubMembershipId>(ClubMembershipId.Unknown)
{
    public UserAccountId MemberAccountId { get; private set; } = UserAccountId.Unknown;
    public SportGroupId SportGroupId { get; private set; } = SportGroupId.Unknown;
    public bool IsMember { get; private set; }
    public bool HasRequestedToJoin { get; private set; }
    public bool JoinRequestAccepted { get; private set; }
    public bool IsInvited { get; private set; }
    public bool HasAcceptedInvitation { get; private set; }

    public void CreateClubMembership(UserAccountId memberAccountId, SportGroupId sportGroupId);
    public void RequestToJoin(JoinPolicyType joinPolicyType);
    public void InvitePerson(JoinPolicyType joinPolicyType);
    public void ApproveJoinRequest(JoinPolicyType joinPolicyType);
    public void AcceptInvitation(JoinPolicyType joinPolicyType);
}
```
As you can see there are 5 methods and the state. So in that situation user have to exist. I pass `JoinPolicyType` as parameter because it is defined in `sportClub` or `sportGroup` level, so it must be passed from there.
I can add a complexity to that soulution to pass `email` or `phone` to `CreateClubMembership` method, but after analyzing the problem I understood that the current solution is fine and I should handle it different way

## Idea of the solution
So if I don't want to change the existing Entity, I need to create something to handle the issue. 
My idea is:
1. When someone is invited to a club. Check if the uses exists.
  - if yes, I can continue processing with: `ClubMembershipEntity`
2. Create `ClubMemberShipInvitationEntity` to keep the information about invitation. It can look like:
    ``` csharp 
    public class ClubMemberShipInvitationEntity() : BaseEntity<ClubMemberShipInvitationEntityId>(ClubMemberShipInvitationEntity.Unknown)
    {
        public SportGroupId SportGroupId { get; private set; } = SportGroupId.Unknown;
        public PersonData InitiatedPersonData {get; private set;}
        public ClubMembershipId? ClubMembershipId {get; private set;}
        public bool IsActive {get; private set;} = true;
        public void InvitePerson(SportGroupId sportGroupId, PersonData initiatedPersonData);
        public void ResolveAsDone(ClubMembershipId clubMembershipId);
        public void Reject();
    }

    public record PersonData(EmailAddress? Email, PhoneNumber? Phone);
    ```
3. With the structure I am able to create the invitation to a group.
4. Then when a user create an account then I can check if there is any waiting invitations for him
  - if yes I can create correct instance of `ClubMembershipEntity`
5. And the process can be continued as usual.

## Summary
Eventually, I decided to not go this way for now, because I am on very early stage of the implementation and we choose simpler solution:
1. That user has to have an account to be invited
2. Someone can create a link to make a request to join to the club, and then user can register itself.

Would you like to add or ask anything? Let me know in the comment section below!
