import { Difficult } from '../enums/dificulity';

export interface Task {
	name: string,
	description: string,
	difficult: Difficult,
	executionTime: number,
	answer: string,
	tags: string[],
	authorName: string;
}