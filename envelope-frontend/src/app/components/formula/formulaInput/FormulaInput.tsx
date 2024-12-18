import { Editor } from "@tinymce/tinymce-react/lib/cjs/main/ts/components/Editor";
import React from "react";
import { FormulaInputProps } from "./FormulaInput.props";

export const FormulaInput = ({
  id,
  reference,
  height,
  placeholder,
  value,
}: FormulaInputProps) => {
  return (
    <Editor
      id={id}
      apiKey={process.env.NEXT_PUBLIC_RICH_EDITOR_KEY}
      onInit={(a, editor) => {
        reference.current = editor;
        if (value) {
          editor.setContent(value);
        }
      }}
      initialValue={value}
      disabled={false}
      init={{
        language: "ru",
        plugins: "math",
        toolbar: "math",
        resize: false,
        height: height + "px",
        placeholder: placeholder,
      }}
    />
  );
};
