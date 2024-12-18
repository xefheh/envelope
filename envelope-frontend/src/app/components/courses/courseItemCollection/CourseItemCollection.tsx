import { CourseItemCollectionProps } from "./CourseItemCollection.props";
import { CourseItem } from "../courseItem/CourseItem";
import styles from "./CourseItemCollection.module.css";

export const CourseItemCollection = ({
  courses,
}: CourseItemCollectionProps): JSX.Element => {
  return (
    <div className={styles.courseContainer}>
      {courses.map((course) => (
        <CourseItem key={course.id} course={course} />
      ))}
    </div>
  );
};
