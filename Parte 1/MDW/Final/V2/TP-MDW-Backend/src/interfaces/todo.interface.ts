import { Schema, Document } from "mongoose";

export interface ITodo extends Document{
  title: string;
  description: string;
  tags: string[]; // tags es un arreglo de cadenas de texto (strings). Es decir, esta variable puede contener múltiples valores de tipo string.
  isPinned: boolean;
  completed: boolean;
  createdOn: Date;
  user: Schema.Types.ObjectId;
}
