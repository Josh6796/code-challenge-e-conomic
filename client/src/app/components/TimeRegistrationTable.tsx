import * as React from 'react';
import { TimeRegistration } from './../models/timeregistration';

export default function TimeRegistrationTable({timeRegistrations}: any) {
    
    const [items, setItems] = React.useState<TimeRegistration[]>([])

    React.useEffect(() => {
        setItems(timeRegistrations);
    }, []);

    return (
        <table className="table-fixed w-full">
            <thead className="bg-gray-200">
                <tr>
                    <th className="border px-4 py-2 w-12">#</th>
                    <th className="border px-4 py-2">Title</th>
                    <th className="border px-4 py-2">Description</th>
                    <th className="border px-4 py-2">TimeSpent</th>
                </tr>
            </thead>
            <tbody>
                {items.map((item) => (
                <tr>
                    <td>{item.timeRegistrationId}</td>
                    <td>{item.title}</td>
                    <td>{item.description}</td>
                    <td>{item.timeSpent}</td>
                </tr>
            ))}
            </tbody>
        </table>
    );
}
