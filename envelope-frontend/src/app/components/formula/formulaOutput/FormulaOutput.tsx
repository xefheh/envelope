"use client";
import { Editor } from "@tinymce/tinymce-react/lib/cjs/main/ts/components/Editor";
import React from "react";
import { FormulaOutputProps } from "./FormulaOutput.props";
import { v4 as uuidv4 } from "uuid";

export const FormulaOutput = ({ id, height, value }: FormulaOutputProps) => {
  return (
    <Editor
      id={id}
      apiKey={process.env.NEXT_PUBLIC_RICH_EDITOR_KEY}
      onInit={(a, editor) => {
        editor.setContent(value);
      }}
      onLoadContent={(a, editor) => {
        editor.setContent(value);
      }}
      onActivate={(a, editor) => {
        editor.setContent(value);
      }}
      disabled={true}
      initialValue={value}
      init={{
        language: "ru",
        plugins: "",
        toolbar: "",
        menubar: false,
        statusbar: false,
        resize: false,
        height: height + "px",
      }}
    />
  );
};
