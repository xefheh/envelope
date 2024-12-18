import { CreateTaskRequest } from '@/app/services/requests/createTaskRequest';
import { Task } from '@/app/types/task';

export interface TaskComponentProps {
	mode: 'edit' | 'create',
	task?: Task | undefined,
	onSubmit(task: CreateTaskRequest): void
}