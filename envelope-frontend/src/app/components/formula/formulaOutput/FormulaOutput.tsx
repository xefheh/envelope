"use client";
import { Editor } from "@tinymce/tinymce-react/lib/cjs/main/ts/components/Editor";
import React from "react";
import { FormulaOutputProps } from "./FormulaOutput.props";
import { v4 as uuidv4 } from "uuid";

export const FormulaOutput = ({ height, value }: FormulaOutputProps) => {
  return (
    <Editor
      id={"input_id_" + uuidv4()}
      apiKey={process.env.NEXT_PUBLIC_RICH_EDITOR_KEY}
      onInit={(_, editor) => editor.setContent(value)}
      disabled={true}
      init={{
        language: "ru",
        plugins: "",
        toolbar: "",
        resize: false,
        height: height + "px",
      }}
    />
  );
};
