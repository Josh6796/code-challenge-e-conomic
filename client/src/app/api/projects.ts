const BASE_URL = "http://localhost:3001/api";

export async function getAll() {
    const response = await fetch(`${BASE_URL}/projects`);
    return response.json();
}

export async function getAllOrderedByDeadline() {
    const response = await fetch(`${BASE_URL}/projects/ordered-by-deadline`);
    return response.json();
}

export async function getById(id: number) {
    const response = await fetch(`${BASE_URL}/projects/${id}`);
    return response.json();
}

export async function addProject(name: string, deadline: string) {
    const requestOptions = {
        method: 'POST'
    };
    const response = fetch(`${BASE_URL}/projects?name=${name}&deadline=${deadline}`, requestOptions)
    return response;
}
export async function registerTime(projectId: number, title: string, description: string, timeSpent: string) {
    const requestOptions = {
        method: 'POST'
    };
    const response = fetch(`${BASE_URL}/projects/register-time?id=${projectId}&Title=${title}&Description=${description}&TimeSpent=${timeSpent}`, requestOptions)
    return response;
}
