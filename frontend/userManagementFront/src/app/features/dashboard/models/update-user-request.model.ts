export interface UpdateUserRequest {
    Id: string,
    FirstName: string,
    LastName: string,
    Email: string,
    Phone: string,
    Role: Role,
    Username: string,
    Password: string,
    Permissions: Permissions[]
}
export interface Permissions {
    PermissionId: string,
    IsReadable: boolean,
    IsWritable: boolean,
    IsDeletable: boolean,
}

export interface Role {
    RoleId: string,
    RoleName: string
}