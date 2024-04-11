export interface AddUserRequest {
    Id: string,
    FirstName: string,
    LastName: string,
    Email: string,
    Phone: string,
    RoleId: string,
    Username: string,
    Password: string,
    // ConfirmPassword: string,
    Permissions: Permissions[]
}

export interface Permissions {
    PermissionId: string,
    IsReadable: boolean,
    IsWritable: boolean,
    IsDeletable: boolean,
}