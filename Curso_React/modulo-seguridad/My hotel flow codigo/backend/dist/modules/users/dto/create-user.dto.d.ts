export declare enum UserRole {
    ADMIN = "admin",
    RECEPCIONISTA = "recepcionista",
    CLIENTE = "cliente"
}
export declare class CreateUserDto {
    username: string;
    email: string;
    password: string;
    fullName?: string;
    role?: UserRole;
    isActive?: boolean;
}
