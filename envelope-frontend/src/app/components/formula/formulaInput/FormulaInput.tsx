import { Editor } from "@tinymce/tinymce-react/lib/cjs/main/ts/components/Editor";
import React from "react";
import { FormulaInputProps } from "./FormulaInput.props";
import { v4 as uuidv4 } from "uuid";

export const FormulaInput = ({
  reference,
  height,
  placeholder,
  value,
}: FormulaInputProps) => {
  return (
    <Editor
      id={"input_id_" + uuidv4()}
      apiKey={process.env.NEXT_PUBLIC_RICH_EDITOR_KEY}
      onInit={(_, editor) => {
        reference.current = editor;
        if (value) {
          editor.setContent(value);
        }
      }}
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
