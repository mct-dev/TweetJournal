export const postEntry = async (content: string) => {
  return await fetch("https://localhost:5555/api/v1/entry", {
    method: "post",
    body: JSON.stringify({ content: content }),
  });
};
