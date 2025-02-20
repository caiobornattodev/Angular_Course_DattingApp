import { User } from "./user";

export class UserParams {

  constructor(user: User | null) {
    this.gender = user?.gender === 'feamale' ? 'male' : 'female'
  }

  gender: string;
  minAge: number = 18;
  maxAge: number = 100;
  pageNumber: number = 1;
  pageSize: number = 5;
  orderBy: string = 'lastActive'
}
