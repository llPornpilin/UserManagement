export interface AddUserRequest {
    userId: string,
    firstName: string,
    lastName: string,
    email: string,
    phone: string,
    role: string,
    username: string,
    password: string,
    confirmPassword: string,
    permissions: Permissions[]
}

interface Permissions {
    permissionId: string,
    isReadable: boolean,
    isWritable: boolean,
    isDeletable: boolean,
}