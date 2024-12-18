import { Task } from '../types/task';
import { TaskSearchModel } from '../types/taskSearchModel';
import { CreateTaskRequest } from './requests/createTaskRequest';
import { GetResponse } from './response/getResponse';

export const postTask = async (task: CreateTaskRequest) => {
	const result = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/tasks/post`, {
		method: 'post',
		body: JSON.stringify({
			...task,
			author: localStorage.getItem('userid')
		}),
		headers: {
		"Content-Type": "application/json",
		"Authorization": `Bearer ${localStorage.getItem('token')}`
	} });
	const id: string = await result.json();
	return id;
}

export const getTasks = async (): Promise<TaskSearchModel[]> => {
	const answer = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/tasks/fetch/getall`, {
		method: 'get',
		headers: {
		"Content-Type": "application/json",
	}});

	const response: GetResponse<TaskSearchModel> = await answer.json();

	const result = response.response.map(value => {
		value.isOurChoose = Math.random() < 0.5;;
		return value;
	})

	return result;
}

export const getTask = async (id: string): Promise<Task> => {
	const answer = await fetch(`${process.env.NEXT_PUBLIC_API_URL}/tasks/fetch/get/${id}`, {
		method: 'get',
		headers: {
		"Content-Type": "application/json",
	}});

	console.log(answer);

	const value: Task = await answer.json();
	value.isOurChoose = Math.random() < 0.5;;
	return value;
} 