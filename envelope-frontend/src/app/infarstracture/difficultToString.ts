import { Difficult } from '@/app/enums/dificulity';

export const difficultToString = (difficult: Difficult): string => {
	switch(difficult) {
		case Difficult.Easy: return 'Легкий';
		case Difficult.Normal: return 'Нормальный';
		case Difficult.Hard: return 'Сложный';
	}
}