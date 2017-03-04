using System;
using System.Collections.Generic;
using System.Linq;

namespace JOS.WorldStatus.Domain
{
	public class Result
	{
		protected Result(bool isSuccess, IEnumerable<Error> errors)
		{
			var err = errors ?? Enumerable.Empty<Error>();
			if (isSuccess && err.Any())
				throw new InvalidOperationException();
			if (!isSuccess && (errors == null || !err.Any()))
				throw new InvalidOperationException();

			IsSuccess = isSuccess;
			Errors = errors;
		}

		public bool IsSuccess { get; }
		public IEnumerable<Error> Errors { get; }
		public bool IsFailure => !IsSuccess;

		public static Result Fail(Error error)
		{
			return Fail(new List<Error> {error});
		}

		public static Result Fail(IEnumerable<Error> errors)
		{
			return new Result(false, errors);
		}

		public static Result<T> Fail<T>(Error error)
		{
			return Fail<T>(new List<Error> {error});
		}

		public static Result<T> Fail<T>(IEnumerable<Error> errors)
		{
			return new Result<T>(default(T), false, errors);
		}

		public static Result Ok()
		{
			return new Result(true, Enumerable.Empty<Error>());
		}

		public static Result<T> Ok<T>(T value)
		{
			return new Result<T>(value, true, Enumerable.Empty<Error>());
		}
	}

	public class Result<T> : Result
	{
		private readonly T value;

		protected internal Result(T value, bool isSuccess, IEnumerable<Error> errors)
			: base(isSuccess, errors)
		{
			this.value = value;
		}

		public T Value
		{
			get
			{
				if (!IsSuccess)
					throw new InvalidOperationException();

				return value;
			}
		}
	}

	public enum ErrorTypes
	{
		Undefined,
		NotFound,
		Forbidden,
		Validation,
		Json,
		Remote
	}
}