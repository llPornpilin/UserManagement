export interface GetUserRequest {
    datasource: GetDatasourse[],
    pageNumber: number,
    pageSize: number,
    totalCount: number
}

export interface GetDatasourse {
    userId: string,
    firstName: string,
    lastName: string,
    email: string,
    role: {},
    userName: string,
    permission: [],
    createdDate: string,
}