const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5128";

export async function httpGet<T>(path: string, query?: Record<string, string>) {
  const url = new URL(path, API_BASE_URL);

  if (query) {
    for (const [k, v] of Object.entries(query)) url.searchParams.set(k, v);
  }

  const res = await fetch(url.toString());

  if (!res.ok) {
    const text = await res.text();
    throw new Error(`GET ${url.pathname} failed (${res.status}): ${text}`);
  }

  return (await res.json()) as T;
}
