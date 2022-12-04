import * as React from 'react';
import { addProject } from '../api/projects';

export default function AddProjectsForm() {

    const [name, setName] = React.useState("");
    const [deadline, setDeadline] = React.useState("");
    const [message, setMessage] = React.useState("");

    let handleSubmit = async (e: any) => {
        e.preventDefault();
        try {
          addProject(name, deadline)
          .then((response) => {
            if (response.status === 200) {
                setName("");
                setDeadline("");
                setMessage("Project created successfully");
            }
            if (response.status === 404) {
                setMessage("Project not found");
            } 
            if (response.status === 400) {
                response.text().then((text) => setMessage(text));
            } 
          });
        } catch (err) {
          console.log(err);
        }
      };
      
    return (
        <form onSubmit={handleSubmit}>
            <input
                className="border rounded-full py-2 px-4"
                type="text"
                placeholder="Name"
                aria-label="Name"
                value = {name}
                onChange={(e) => setName(e.target.value)}
            />
            <input
                className="border rounded-full py-2 px-4"
                type="date"
                placeholder="Deadline"
                aria-label="Deadline"
                value = {deadline}
                onChange={(e) => setDeadline(e.target.value)}
            />
            <button className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4"
                    type="submit">
                    Add Project
            </button>

            <div className="message">{message ? <p>{message}</p> : null}</div>
        </form>
    );
}
