import { Difficult } from '@/app/enums/dificulity';

export interface CreateTaskRequest {
	name: string,
	description: string,
	difficult: Difficult,
	executionTime: number,
	answer: string,
	tags: string[],
}