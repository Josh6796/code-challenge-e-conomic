export class Project {

    id: number;
    name: string;
    deadline: Date;
    complete: boolean;
    timeRegistrations: [];

    constructor(id: number, name: string, deadline: Date, complete: boolean, timeRegistrations: []) {
        this.id = id;
        this.name = name;
        this.deadline = deadline;
        this.complete = complete;
        this.timeRegistrations = timeRegistrations;
    }
  }