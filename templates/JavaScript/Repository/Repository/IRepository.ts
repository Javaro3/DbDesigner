import { InferAttributes, Model, WhereOptions } from "sequelize";
import { MakeNullishOptional } from "sequelize/lib/utils";

export interface IRepository<T extends Model> {

  create(entity: MakeNullishOptional<T['_creationAttributes']>): Promise<T>;

  findById(id: string | number): Promise<T | null>;

  findAll(filter?: any): Promise<T[]>;

  update(id: string | number, updates: Partial<T>): Promise<T | null>;

  delete(id: string | number): Promise<boolean>;

  exists(filter: WhereOptions<InferAttributes<T>>): Promise<boolean>;
}