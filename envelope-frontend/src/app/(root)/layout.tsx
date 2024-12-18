import { Metadata } from "next";
import { Layout } from "../components/layout/Layout";
import "../globals.css";

export const metadata: Metadata = {
  title: "Envelope Education: Образование",
  description:
    "Главная страница сайта Envelope Education, занимающийся продвижением курсов и задач",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="ru">
      <head>
        <link
          rel="stylesheet"
          href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200&icon_names=login"
        />
      </head>
      <body>
        <Layout>{children}</Layout>
      </body>
    </html>
  );
}
