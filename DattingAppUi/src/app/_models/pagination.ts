export interface Pagination {
  currentPage: number;
  itensPerPage: number;
  totalItems: number;
  totalPages: number;
}

export class PaginatedResult<T> {
  items?: T;
  pagination?: Pagination
}
