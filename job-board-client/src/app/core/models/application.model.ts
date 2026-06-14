/* A strict union of application statuses matching the backend's ApplicationStatus Enum */
export type ApplicationStatus = 'Pending' | 'Approved' | 'Rejected';

export interface JobApplication {
    id: string;
    jobId: string;
    jobTitle?: string;       /* Optional: Helpful for displaying applied job lists to candidates */
    companyName?: string;    /* Optional: Helpful for displaying applied job lists to candidates */
    candidateId: string;
    candidateName?: string;  /* Optional: Helpful for recruiters to see who applied */
    resumePath: string;      /* Path to the uploaded PDF CV in the server uploads folder */
    status: ApplicationStatus;
    appliedDate: string;     /* ISO date representation of when the application was made */
}