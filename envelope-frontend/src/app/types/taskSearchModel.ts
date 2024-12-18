import { Difficult } from '../enums/dificulity';

export interface TaskSearchModel {
	id: string;
	name: string;
	tags: string[];
	difficult: Difficult
	isOurChoose: boolean;
}