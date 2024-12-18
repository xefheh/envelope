"use client";

import { useState, useEffect } from "react";
import { CourseItemCollection } from "../components/courses/courseItemCollection/CourseItemCollection";
import { Preloader } from "../components/preloader/Preloader";
import { getCourses } from "../services/courseService";
import { CourseSearchModel } from "../types/courseSearchModel";

export default function Home(): JSX.Element {
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
