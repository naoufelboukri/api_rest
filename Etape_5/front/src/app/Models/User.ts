import { Address } from "./Address";

export class User {
    id: number;
    username: string;
    password: string;
    role: string;
    addresses: Address[];
}