import { CourseItemProps } from "./CourseItem.props";
import styles from "./CourseItem.module.css";
import { TagCollection } from "@/app/components/tags/tagCollection/TagCollection";
import starEmpty from "@/app/assets/imgs/stars/starEmpty.png";
import starFill from "@/app/assets/imgs/stars/starFill.png";
import Image from "next/image";
import cn from "classnames";

export const CourseItem = ({ course }: CourseItemProps): JSX.Element => {
  const starsTemplate = [0, 0, 0, 0, 0].map((_, idx) => idx < course.mark);
  return (
    <div
      className={cn(styles.courseCard, {
        [styles.isOurChoose]: course.isOurChoose,
      })}
    >
      <div className={styles.leftColumn}>
        <div className={styles.courseInfo}>
          <h1 className={styles.courseTitle}>{course.name}</h1>
          <p className={styles.courseDescription}>{course.description}</p>
        </div>
        <TagCollection tags={course.tags} />
      </div>
      <div
        className={cn(styles.rightColumn, {
          [styles.nonOurChoose]: !course.isOurChoose,
        })}
      >
        <h2 className={styles.starsHeader}>Оценка</h2>
        <div className={styles.stars}>
          {starsTemplate.map((item, idx) =>
            item ? (
              <Image
                key={idx}
                src={starFill}
                alt={"Есть оценка:" + idx + 1}
                width={30}
                height={30}
              />
            ) : (
              <Image
                key={idx}
                src={starEmpty}
                alt={"Нет оценки:" + idx + 1}
                width={30}
                height={30}
              />
            )
          )}
        </div>
        <p className={styles.markCount}>{course.markCount ?? "нет"} отзывов.</p>
      </div>
    </div>
  );
};
