export type UserRole = 'Candidate' | 'Recruiter' | 'Admin';

export interface User {
    id: string;
    fullName: string;
    email: string;
    role: UserRole;
}
