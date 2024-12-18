import { useRouter } from 'next/router';
import { CourseSearchModel } from '../types/courseSearchModel';
import { GetResponse } from './response/getResponse';

export const getCourses = async (): Promise<CourseSearchModel[]> => {
	const answer = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/courses`, {
		method: 'get',
		headers: {
		"Content-Type": "application/json",
	}});

	const response: GetResponse<CourseSearchModel> = await answer.json();

	const result = response.response.map(value => {
		value.isOurChoose = Math.random() < 0.5;;
		value.tags = ['Анимешки', 'Прикольчики', 'Барабульчики'];
		return value;
	})

	return result;
}