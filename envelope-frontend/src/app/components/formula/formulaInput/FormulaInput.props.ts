import { MutableRefObject } from 'react';

export interface FormulaInputProps {
	reference: MutableRefObject<any | null>;
	height: number;
	placeholder: string;
	value?: string;
}