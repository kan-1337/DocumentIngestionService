const API_BASE_URL =
  import.meta.env.VITE_API_BASE_URL ?? "http://localhost:5128";

function getAuthHeaders(): HeadersInit {
  const stored = localStorage.getItem("auth_user");
  if (stored) {
    const user = JSON.parse(stored);
    console.log("Auth token:", user.token.substring(0, 50) + "...");
    return {
      Authorization: `Bearer ${user.token}`,
    };
  }
  console.warn("No auth token found in localStorage");
  return {};
}

export async function httpGet<T>(path: string, query?: Record<string, string>) {
  const url = new URL(path, API_BASE_URL);

  if (query) {
    for (const [k, v] of Object.entries(query)) url.searchParams.set(k, v);
  }

  const res = await fetch(url.toString(), {
    headers: getAuthHeaders(),
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(`GET ${url.pathname} failed (${res.status}): ${text}`);
  }

  return (await res.json()) as T;
}

export async function httpDelete(path: string) {
  const url = new URL(path, API_BASE_URL);

  const res = await fetch(url.toString(), {
    method: "DELETE",
    headers: getAuthHeaders(),
  });

  if (!res.ok) {
    const text = await res.text();
    throw new Error(`DELETE ${url.pathname} failed (${res.status}): ${text}`);
  }
}
