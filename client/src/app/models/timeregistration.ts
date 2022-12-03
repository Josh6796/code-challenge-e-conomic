export class TimeRegistration {

    timeRegistrationId: number;
    title: string;
    description: string;
    timeSpent: number;

    constructor(timeRegistrationId: number, title: string, description: string, timeSpent: number) {
        this.timeRegistrationId = timeRegistrationId;
        this.title = title;
        this.description = description;
        this.timeSpent = timeSpent;
    }
  }