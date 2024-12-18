export interface CourseSearchModel {
	id: string;
	name: string;
	description: string;
	mark: number;
	markCount: number;
	tags: string[];
	isOurChoose: boolean;
}