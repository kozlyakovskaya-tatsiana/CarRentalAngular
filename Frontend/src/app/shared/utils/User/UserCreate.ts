import {UserBase} from './UserBase';

export class UserCreate extends UserBase{
  password: string;
  role: string;
  email: string;
}
