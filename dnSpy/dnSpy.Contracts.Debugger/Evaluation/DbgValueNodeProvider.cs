﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Threading;
using dnSpy.Contracts.Debugger.CallStack;

namespace dnSpy.Contracts.Debugger.Evaluation {
	/// <summary>
	/// Provides <see cref="DbgValueNode"/>s for the variables windows
	/// </summary>
	public abstract class DbgValueNodeProvider {
		/// <summary>
		/// Gets the language
		/// </summary>
		public abstract DbgLanguage Language { get; }

		/// <summary>
		/// Gets all values. It blocks the current thread until the method is complete.
		/// The returned <see cref="DbgValueNode"/>s are automatically closed when their runtime continues.
		/// </summary>
		/// <param name="context">Evaluation context</param>
		/// <param name="frame">Frame</param>
		/// <param name="options">Options</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns></returns>
		public abstract DbgValueNode[] GetNodes(DbgEvaluationContext context, DbgStackFrame frame, DbgValueNodeEvaluationOptions options, CancellationToken cancellationToken = default);
	}

	/// <summary>
	/// Locals value node provider options
	/// </summary>
	[Flags]
	public enum DbgLocalsValueNodeEvaluationOptions {
		/// <summary>
		/// No bit is set
		/// </summary>
		None								= 0,

		/// <summary>
		/// Show compiler generated variables (<see cref="DebuggerSettings.ShowCompilerGeneratedVariables"/>)
		/// </summary>
		ShowCompilerGeneratedVariables		= 0x00000001,

		/// <summary>
		/// Show decompiler generated variables (<see cref="DebuggerSettings.ShowDecompilerGeneratedVariables"/>)
		/// </summary>
		ShowDecompilerGeneratedVariables	= 0x00000002,
	}

	/// <summary>
	/// Provides <see cref="DbgValueNode"/>s for the locals window
	/// </summary>
	public abstract class DbgLocalsValueNodeProvider {
		/// <summary>
		/// Gets the language
		/// </summary>
		public abstract DbgLanguage Language { get; }

		/// <summary>
		/// Gets all values. It blocks the current thread until the method is complete.
		/// The returned <see cref="DbgValueNode"/>s are automatically closed when their runtime continues.
		/// </summary>
		/// <param name="context">Evaluation context</param>
		/// <param name="frame">Frame</param>
		/// <param name="options">Options</param>
		/// <param name="localsOptions">Locals value node provider options</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns></returns>
		public abstract DbgLocalsValueNodeInfo[] GetNodes(DbgEvaluationContext context, DbgStackFrame frame, DbgValueNodeEvaluationOptions options, DbgLocalsValueNodeEvaluationOptions localsOptions, CancellationToken cancellationToken = default);
	}

	/// <summary>
	/// Value node kind
	/// </summary>
	public enum DbgLocalsValueNodeKind {
		/// <summary>
		/// Unknown value
		/// </summary>
		Unknown,

		/// <summary>
		/// Parameter value
		/// </summary>
		Parameter,

		/// <summary>
		/// Local value
		/// </summary>
		Local,

		/// <summary>
		/// Error value
		/// </summary>
		Error,
	}

	/// <summary>
	/// Contains a value node and its kind
	/// </summary>
	public struct DbgLocalsValueNodeInfo {
		/// <summary>
		/// What kind of value this is (local or parameter)
		/// </summary>
		public DbgLocalsValueNodeKind Kind { get; }

		/// <summary>
		/// Gets the node
		/// </summary>
		public DbgValueNode ValueNode { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="kind">What kind of value this is (local or parameter)</param>
		/// <param name="valueNode">Value node</param>
		public DbgLocalsValueNodeInfo(DbgLocalsValueNodeKind kind, DbgValueNode valueNode) {
			Kind = kind;
			ValueNode = valueNode ?? throw new ArgumentNullException(nameof(valueNode));
		}
	}
}
