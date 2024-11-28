import { Task } from '../types/task';

export const postTask = async (task: Task) => {
	await fetch(`${process.env.NEXT_PUBLIC_API_URL}/tasks/post`, {
		method: 'post',
		body: JSON.stringify({
			...task,
			author: '3fa85f64-5717-4562-b3fc-2c963f66afa6'
		}),
		headers: {
		"Content-Type": "application/json",
	} });
}