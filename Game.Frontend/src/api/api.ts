export async function apiFetch(url: string, options: any = {}) {

    const token = localStorage.getItem("token");
  
    const response = await fetch(url, {
      ...options,
      headers: {
        "Content-Type": "application/json",
        ...(token && { Authorization: `Bearer ${token}` }),
        ...options.headers
      }
    });
  
    if (response.status === 401) {
      localStorage.removeItem("token");
      if (window.location.pathname !== "/") {
          window.location.href = "/";
      }
      throw new Error("Unauthorized");
    }
    
    if (!response.ok) {
      throw new Error(`Request failed with status ${response.status}`);
    }
  
    return response.json();
  }