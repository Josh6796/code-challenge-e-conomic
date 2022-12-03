import * as React from 'react';
import { registerTime } from '../api/projects';

export default function RegisterTimeForm({ projectId }: any) {

    const [title, setTitle] = React.useState("");
    const [description, setDescription] = React.useState("");
    const [timeSpent, setTimeSpent] = React.useState("");
    const [message, setMessage] = React.useState("");

    let handleSubmit = async (e: any) => {
        e.preventDefault();
        try {
          registerTime(projectId, title, description, timeSpent)
          .then((response) => {
            if (response.status === 200) {
                setTitle("");
                setDescription("");
                setTimeSpent("");
                setMessage("Time registered successfully");
            } 
            if (response.status === 404) {
                setMessage("Project not found");
            } 
            if (response.status === 400) {
                response.text().then((text) => setMessage(text));
            } 
            else {
                setMessage("Something went wrong");
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
                placeholder="Title"
                aria-label="Title"
                value = {title}
                onChange={(e) => setTitle(e.target.value)}
            />
            <input
                className="border rounded-full py-2 px-4"
                type="text"
                placeholder="Description"
                aria-label="Description"
                value = {description}
                onChange={(e) => setDescription(e.target.value)}
            />
            <input
                className="border rounded-full py-2 px-4"
                type="number"
                placeholder="Time Spent (minutes)"
                aria-label="Time Spent (minutes)"
                value = {timeSpent}
                onChange={(e) => setTimeSpent(e.target.value)}
            />
            <button className="bg-blue-500 hover:bg-blue-700 text-white rounded-full py-2 px-4"
                    type="submit">
                    Register Time
            </button>

            <div className="message">{message ? <p>{message}</p> : null}</div>
        </form>
    );
}
