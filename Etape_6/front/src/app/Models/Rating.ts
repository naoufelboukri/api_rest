import { Post } from "./Post";

export class Rating {
    id: number;
    content: Text;
    value: number;
    userId: number;
    postId: number;
    created_at: Date;
}