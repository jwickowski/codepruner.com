$path = "C:\Projects\jw\codepruner.com\src\codepruner.com\content\posts"
$items = Get-ChildItem -Path $path -Recurse -Filter *.md
foreach($item in $items)
{
    $fileName = $item.FullName
    
    $url = $fileName.Replace("C:\Projects\jw\codepruner.com\src\codepruner.com\content\","")
    $url = $url.Replace(".md","")
    $url = $url.Replace("\","/")

    $category = $url.Replace("posts/","");
    $category = $category.Substring(0,$category.IndexOf("/"))
    $category = $category.ToLower()
    
    #Write-Host $category

    (Get-Content $fileName) | 
    Foreach-Object {
        $_ # send the current line to output
        if ($_ -match "categories:") 
        {
            #Add Lines after the selected pattern 
            #"url: ""$url"""
            #"images:"
            #"  - ""images/blog/red-sharp-pruner.jpg"""
            #"author: ""Jerzy Wickowski"""
           # "categories: [""$category""]"
            "type: ""regular"""
        }
    } | Set-Content $fileName


}


Write-Host $items.Length