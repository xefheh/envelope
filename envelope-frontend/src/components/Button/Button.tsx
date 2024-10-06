import { ButtonProps } from "./Button.props";
import cn from "classnames";
import styles from "./Button.module.scss";

export const Button = ({className, children, appearance = 'default', ...attributes}: ButtonProps): JSX.Element => {
    return <button className={cn(styles.btn, {
        [styles.primary]: appearance === 'primary',
        [styles.secondary]: appearance === 'secondary',
        [styles.default]: appearance === 'default'
    })}

            {...attributes}>
        {children}
    </button>

}