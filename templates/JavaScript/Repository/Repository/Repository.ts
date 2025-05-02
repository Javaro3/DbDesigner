import { Attributes, InferAttributes, Model, ModelStatic, WhereOptions } from 'sequelize';
import { IRepository } from './IRepository';
import { MakeNullishOptional } from 'sequelize/lib/utils';

export abstract class Repository<T extends Model> implements IRepository<T> {
  protected model: ModelStatic<T>;

  constructor(model: ModelStatic<T>) {
    this.model = model;
  }

  async create(entity: MakeNullishOptional<T['_creationAttributes']>): Promise<T> {
    try {
      return await this.model.create(entity);
    } catch (error) {
      throw new Error(`Error creating entity: ${error instanceof Error ? error.message : String(error)}`);
    }
  }

  async findById(id: string | number): Promise<T | null> {
    try {
      return await this.model.findByPk(id);
    } catch (error) {
      throw new Error(`Error finding entity by ID: ${error instanceof Error ? error.message : String(error)}`);
    }
  }

  async findAll(filter?: any): Promise<T[]> {
    try {
      return await this.model.findAll(filter);
    } catch (error) {
      throw new Error(`Error finding all entities: ${error instanceof Error ? error.message : String(error)}`);
    }
  }

  async update(id: string | number, updates: Partial<T>): Promise<T | null> {
    try {
      const entity = await this.model.findByPk(id);
      if (!entity) return null;

      await entity.update(updates);
      return entity;
    } catch (error) {
      throw new Error(`Error updating entity: ${error instanceof Error ? error.message : String(error)}`);
    }
  }

  async delete(id: string | number): Promise<boolean> {
    try {
      const where: WhereOptions<Attributes<T>> = { 
        id: id as NonNullable<Attributes<T>['id']> 
      };
      const deletedCount = await this.model.destroy({ where });
      return deletedCount > 0;
    } catch (error) {
      throw new Error(`Error deleting entity: ${error instanceof Error ? error.message : String(error)}`);
    }
  }

  async exists(filter: WhereOptions<InferAttributes<T>>): Promise<boolean> {
    try {
      const count = await this.model.count({ 
        where: filter as WhereOptions<Attributes<T>> 
      });
      return count > 0;
    } catch (error) {
      throw new Error(`Error checking entity existence: ${error instanceof Error ? error.message : String(error)}`);
    }
  }
}