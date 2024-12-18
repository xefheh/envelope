import { TagCollectionProps } from "./TagCollection.props";
import styles from "./TagCollection.module.css";

export const TagCollection = ({ tags }: TagCollectionProps): JSX.Element => {
  return (
    <div className={styles.tagsContainer}>
      {tags.map((tag) => (
        <div key={tag} className={styles.tag}>
          {tag}
        </div>
      ))}
    </div>
  );
};
