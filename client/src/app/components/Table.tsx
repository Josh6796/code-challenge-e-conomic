import * as React from 'react';
import Popup from 'reactjs-popup';
import { getAll, getAllOrderedByDeadline } from "../api/projects";
import { Project } from './../models/project';
import RegisterTimeForm from './RegisterTimeForm';
import TimeRegistrationTable from './TimeRegistrationTable';

export default function Table() {
    const [items, setItems] = React.useState<Project[]>([])

    React.useEffect(() => {
        let isSubscribed = true
        getAll().then( data => {
          if (isSubscribed) {
            setItems(data)
          }
        })
        return () => { 
            isSubscribed = false
        }
      }, [])

    let handleOrderByDeadline = async () => {
        getAllOrderedByDeadline().then( data => {
                setItems(data)
        })
    };

    return (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">#</th>
                    <th className="border px-4 py-2">Project Name</th>
                    <th className="border px-4 py-2">Deadline</th>
                    <th className="border px-4 py-2">Complete</th>
                    <th className="border px-4 py-2"></th>
                    <th className="border px-4 py-2">
                        <button 
                        className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" 
                        onClick={handleOrderByDeadline}>Order by Deadline</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                {items.map((item) => (
                <tr>
                    <td>{item.id}</td>
                    <td>{item.name}</td>
                    <td>{new Date(item.deadline).toLocaleDateString()}</td>
                    <td>{item.complete.toString()}</td>
                    <td>                
                        <Popup trigger={
                            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded">
                                Show Time Registrations
                            </button>} position="right center">
                            <TimeRegistrationTable timeRegistrations={item.timeRegistrations} />
                        </Popup>
                    </td>
                    <td>                
                        <Popup trigger={
                            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" hidden={item.complete}>
                                Register Time
                            </button>} position="right center">
                            <RegisterTimeForm projectId={item.id} />
                        </Popup>
                    </td>
                </tr>
            ))}
            </tbody>
        </table>
    );
}
