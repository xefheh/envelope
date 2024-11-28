import { Task } from '@/app/types/task';

export interface TaskComponentProps {
	mode: 'edit' | 'create',
	task?: Task | undefined,
	onSubmit(task: Task): void
}