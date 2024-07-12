export interface RegisterUserRequest
{
    username: string
    passwordHash: string
    email: string
    name: string
    location: string
    dateJoined: string
}