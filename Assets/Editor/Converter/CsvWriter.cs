using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class CsvWriter : IDisposable
{
	/// <summary>CSVファイルに書き込むストリーム</summary>
	private StreamWriter stream = null;

	/// <summary>
	/// ファイル名を指定して、 <see cref="CsvWriter">CsvWriter</see> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="path">書き込む完全なファイルパス。</param>
	public CsvWriter(string path) :
		this(path, Encoding.Default)
	{
	}
	/// <summary>
	/// ファイル名、文字エンコーディングを指定して、 <see cref="CsvWriter">CsvWriter</see> クラスの新しいインスタンスを初期化します。
	/// </summary>
	/// <param name="path">書き込む完全なファイルパス。</param>
	/// <param name="encoding">使用する文字エンコーディング。</param>
	public CsvWriter(string path, Encoding encoding)
	{
		var stream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);
		this.stream = new StreamWriter(stream, encoding);
	}

	/// <summary>
	/// 使用する文字エンコーディングを取得します。
	/// </summary>
	public Encoding Encoding
	{
		get
		{
			return this.stream.Encoding;
		}
	}

	/// <summary>
	/// 現在のストリームで利用される改行文字列を取得または設定します。
	/// </summary>
	public string NewLine
	{
		get
		{
			return this.stream.NewLine;
		}

		set
		{
			this.stream.NewLine = value;
		}
	}

	/// <summary>
	/// 現在のストリームオブジェクトと基になるストリームをとじます。
	/// </summary>
	public void Close()
	{
		if (this.stream == null)
		{
			return;
		}

		this.stream.Close();
	}

	/// <summary>
	/// CsvWriter で利用されているすべてのリソースを解放します。
	/// </summary>
	public void Dispose()
	{
		if (this.stream == null)
		{
			return;
		}

		this.stream.Close();
		this.stream.Dispose();
		this.stream = null;
	}

	/// <summary>
	/// 現在のライターで使用したすべてのバッファーをクリアし、バッファー内のすべてのデータをストリームに書き込みます。
	/// </summary>
	public void Flush()
	{
		this.stream.Flush();
	}

	/// <summary>
	/// ストリームに文字を書き込みます。
	/// </summary>
	/// <typeparam name="T">リストの型。</typeparam>
	/// <param name="data">CSVデータ。</param>
	public void Write(List<String>[] data, int sheetlast)
	{
		var j = 0;
		for (int i = 0; i < sheetlast + 1; ++i)
		{
			foreach (var row in data)
			{
				if (j == data.Length - 1)
				{
					this.WriteRow(row[i], true, (i == sheetlast));
					j = 0;
				}
				else
				{
					this.WriteRow(row[i], false);
					++j;
				}
			}
		}
	}

	/// <summary>
	/// ストリームに文字を書き込みます。
	/// </summary>
	/// <typeparam name="T">リストの型。</typeparam>
	/// <param name="data">CSVデータ。</param>
	/// <param name="isNewLine">改行するか</param>
	public void Write(List<String> data, bool isNewLine)
	{
		for(int i= 0; i < data.Count; ++i)
		{
			this.WriteRow(data[i], isNewLine, (i == data.Count-1));
		}
	}

	/// <summary>
	/// ストリームに１レコード分の文字列を書き込みます。
	/// </summary>
	/// <typeparam name="T">リストの型。</typeparam>
	/// <param name="row">CSVの１レコード。</param>
	public void WriteRow(string row, bool isNewLine, bool isEnd = false)
	{
		var sb = new StringBuilder();
		sb.Append(row);

		if (isNewLine == true) { sb.Append(System.Environment.NewLine); }
		else if (!isEnd)
		{
			sb.Append(",");
		}
		this.stream.Write(sb.ToString());
	}
}