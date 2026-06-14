export interface Job {
    id?: string;
    title: string;
    description: string;
    location: string;
    salary: number;
    applicationDeadline?: string;
    createdById?: string;
}