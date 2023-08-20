---
title: "how-to-adapt-generated-apiClient-with-xxxx"
date: 2023-08-20T10:00:00+01:00
draft: true
tags: ["javascript", "typescript", "apiClient", "api", "generator", "ongoing refactor", "code generator"]
---
 
# s sd
```

const HTTP_UNAUTHORIZED = 401;

type ApiClientParameters = {
  api: GeneratedApiClient<unknown>;
  requestParams: RequestParams;
};
type ApiClientOptions<ResponseType> = (
  apiClientParameters: ApiClientParameters
) => Promise<AxiosResponse<ResponseType>>;


export const apiClient = <ResponseType>(
  func: ApiClientOptions<ResponseType>,
  requestOptions: RequestOptions = {}
): Promise<AxiosResponse<ResponseType>> => {
  const requestParms: RequestParams = {};


  const apiClientInstance = createApiClient();
  const queryPromise = func({
    api: apiClientInstance,
    requestParams: requestParms,
  });

return queryPromise;
 
};





export const getUserCards = async (
  getUserCardsParams: GetUserCardsParams
): Promise<UserCardDto[]> => {
  const response = await apiClient(({ api, requestParams }) =>
    api.cards.userCardsList(getUserCardsParams, requestParams)
  );

  return response.data;
};


const createApiClient = (): GeneratedApiClient<unknown> => {
  const baseUrl = getBaseUrl();

  const httpClient = new HttpClient({
    baseURL: baseUrl,
    headers: {
      "X-api-version": "2.0",
      "content-type": "application/json;charset=UTF-8",
    },
    securityWorker,
  });
  const result = new GeneratedApiClient(httpClient);
  return result;
};

const securityWorker = <SecurityDataType = unknown>(
  securityData: SecurityDataType | null
): AxiosRequestConfig => {
  const result: AxiosRequestConfig = {};
  const state = store.getState();
  const userData = authenticatedUserDataSelector(state.AuthReducer);
  if (userData.isAuthenticated) {
    result.headers = { Authorization: state.AuthReducer.token ?? "" };
  }
  return result;
};



```