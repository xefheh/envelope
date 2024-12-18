"use client";

import { CourseItemCollection } from "@/app/components/courses/courseItemCollection/CourseItemCollection";
import { Preloader } from "@/app/components/preloader/Preloader";
import { getCourses } from "@/app/services/courseService";
import { CourseSearchModel } from "@/app/types/courseSearchModel";
import { useEffect, useState } from "react";

export default function CourseCollectionPage(): JSX.Element {
  const [courses, setCourses] = useState<CourseSearchModel[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    getCourses().then((courses) => {
      setCourses(courses);
      setIsLoading(false);
    });
  }, []);

  return (
    <>
      {isLoading ? <Preloader /> : <CourseItemCollection courses={courses} />}
    </>
  );
}
