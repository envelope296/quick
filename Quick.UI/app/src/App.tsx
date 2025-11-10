import { useState, type ChangeEvent } from "react";

export default function App() {
  const [text, setText] = useState<string>("");

  const handleFile = (e: ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (!file) return;

    const reader = new FileReader();

    reader.onload = (event: ProgressEvent<FileReader>) => {
      const result = event.target?.result;
      if (typeof result === "string") {
        setText(result);
      }
    };

    reader.readAsText(file); // читаем файл как текст
  };

  return (
    <div>
      <input type="file" accept=".txt" onChange={handleFile} />
      <pre>{text}</pre>
    </div>
  );
}