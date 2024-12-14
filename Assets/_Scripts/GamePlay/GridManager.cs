using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    // Singleton instance
    //public static GridManager Instance { get; private set; }

    public LevelData levelData;
    public BusPrefabs busPrefabs;

    private int rows = 0;
    private int cols = 0;

    public GridCell[,] gridCells;
    public GameObject whiteTilePrefab; // Prefab cho ô trắng
    public GameObject blackTilePrefab; // Prefab cho ô đen
    public GameObject singleSeatPrefab;
    public GameObject doubleSeatPrefab;
    public GameObject tripleSeatPrefab;
    private Vector3 thickTilePrefab;
    private Vector3 correction;
    public GameObject bus;
    public string doneGrid = null;
    public string doneBus = null;
    public string doneLevelData = null;
    private int numberSeat = 1; //for seatnamme
    public Transform[] edges = new Transform[4]; //right, left, top, bottom



    IEnumerator Start()
    {

        if (LevelLoader.Instance != null && LevelLoader.Instance.currentLevelData != null)
        {
            levelData = LevelLoader.Instance.currentLevelData;
        }
        else
        {
            // Nếu chạy trực tiếp scene gameplay, giữ nguyên dữ liệu từ inspector
            Debug.Log("Using inspector assigned levelData");
        }
        //int level = GetLevel(levelData.name);
        //Debug.Log(level);

        Vector2 busSize = BusInfo.GetBusSize(levelData.busType);
        rows = (int)busSize.x;
        cols = (int)busSize.y;
        doneLevelData = "doneLevelData";

        //Generate bus
        yield return new WaitUntil(() => BusManager.Instance.doneAllBusPosition != "");
        bus.transform.position = BusManager.Instance.startBusPosition;
        GameObject busPrefab = busPrefabs.GetPrefab(levelData.busType);
        GameObject busObject = Instantiate(busPrefab, bus.transform.position 
            + Vector3.down * bus.transform.position.y, Quaternion.identity, bus.transform);
        busObject.name = "BusPrefab";

        //Generate grid tile
        gridCells = new GridCell[rows, cols];
        thickTilePrefab = new Vector3(0, whiteTilePrefab.transform.localScale.y, 0);
        //correction = new Vector3(-(float)cols+ 0.5f, 0, -(float)rows - 0.5f);
        //correction = new Vector3(-(float)cols + 1f, 0, -(float)rows);
        correction = new Vector3(-(float)cols / 2 + 0.5f, 0, -(float)rows / 2 - 0.5f);
        doneGrid = GenerateGrid();

        //generate 4 side of bus
        Vector3 busPos = bus.transform.position;
        edges[0].position = new Vector3(busPos.x + (float)(cols + 1) / 2, busPos.y + 1, busPos.z);
        edges[0].localScale = new Vector3(1, 1, rows);
        edges[1].position = new Vector3(busPos.x - (float)(cols + 1) / 2, busPos.y + 1, busPos.z);
        edges[1].localScale = new Vector3(1, 1, rows);
        edges[2].position = new Vector3(busPos.x, busPos.y + 1, busPos.z + (float)(rows + 1) / 2);
        edges[2].localScale = new Vector3(cols, 1, 1);
        edges[3].position = new Vector3(busPos.x, busPos.y + 1, busPos.z - (float)(rows + 1) / 2);
        edges[3].localScale = new Vector3(cols, 1, 1);

        doneBus = "doneBus";
    }

    string GenerateGrid()
    {
        Transform floor = bus.transform.Find("Floor");
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Vector3 position = new Vector3(c, thickTilePrefab.y/2, rows - r) + bus.transform.position + correction;
                GameObject tilePrefab = (r + c) % 2 == 0 ? whiteTilePrefab : blackTilePrefab;
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, floor);
                gridCells[r, c] = new GridCell(tile, r, c);
                tile.name = $"r{r}_{c}";
            }
        }
        return "done";
    }

    public bool PlaceSeatOnGrid(Seat seat, int startRow, int startCol)
    {
        List<GridCell> cells = GetCellsForSeat(startRow, startCol, seat); // Lấy các ô mà ghế sẽ chiếm
        foreach (var cell in cells)
        {
            if (cell.isOccupied)
            {
                Debug.Log($"ô r{startRow}_{startCol} đã có ghế, không thể đặt");
                return false;
            }// Nếu bất kỳ ô nào bị chiếm thì không thể đặt
        }
        // Tạo ghế từ prefab tương ứng
        GameObject seatPrefab = null;
        switch (seat)
        {
            case SingleSeat:
                seatPrefab = singleSeatPrefab;
                break;
            case DoubleSeat:
                seatPrefab = doubleSeatPrefab;
                break;
            case TripleSeat:
                seatPrefab = tripleSeatPrefab;
                break;
        }
        // Kiểm tra nếu seatPrefab đã được gán giá trị
        if (seatPrefab == null)
        {
            Debug.LogError("Không tìm thấy prefab cho loại ghế này.");
            return false;
        }

        // Instantiate (tạo) đối tượng ghế từ prefab
        Vector3 seatPosition = cells[0].tile.transform.position + thickTilePrefab/2; // Vị trí của ô đầu tiên mà ghế sẽ chiếm
        Quaternion seatRotation = Quaternion.identity; // Có thể chỉnh sửa nếu cần xoay ghế
        switch (seat.direction)
        {
            case Direction.Up:
                seatRotation = Quaternion.Euler(0, 0, 0); // Hướng lên
                break;
            case Direction.Down:
                seatRotation = Quaternion.Euler(0, 180, 0); // Hướng xuống
                break;
            case Direction.Left:
                seatRotation = Quaternion.Euler(0, 270, 0); // Hướng trái
                break;
            case Direction.Right:
                seatRotation = Quaternion.Euler(0, 90, 0); // Hướng phải
                break;
        }
        Transform allSeats = bus.transform.Find("AllSeats");
        GameObject seatObject = Instantiate(seatPrefab, seatPosition, seatRotation, allSeats);
        seatObject.name = $"seat{numberSeat}";
        seatObject.GetComponent<SeatDataa>().seat = seat; //gắn dữ liệu cho thuộc tính seat trong component SeatData
        numberSeat++;
        // Cập nhật màu sắc ghế
        ColorSeat(seatObject, seat);
        // đặt để không nhìn thấy seat tạo hiệu ứng nổi bọt:
        seatObject.transform.localScale = Vector3.zero;

        // Đặt ghế vào các ô trống
        foreach (var cell in cells)
        {
            cell.isOccupied = true;
            cell.objectOncell = seat;
        }
        seat.currentRow = startRow;
        seat.currentCol = startCol;

        // Lưu trữ thông tin đối tượng ghế trong seat để tham chiếu sau này
        seat.associatedObject = seatObject;

        return true;
    }
    

    //Hàm di chuyển ghế trong grid
    public bool MoveSeat(int newRow, int newCol, Seat seat)
    {
        //giải phóng cells cũ đề phòng đặt ghế lên cells mà các cells gối lên cells cũ
        List<GridCell> oldCells = GetCellsForSeat(seat.currentRow, seat.currentCol, seat);
        foreach (var cell in oldCells)
        {
            cell.isOccupied = false;
            cell.objectOncell = null;
        }

        List<GridCell> newCells = GetCellsForSeat(newRow, newCol, seat);

        // Cập nhật vị trí của đối tượng ghế
        Vector3 newSeatPosition = newCells[0].tile.transform.position + thickTilePrefab/2;
        seat.associatedObject.transform.position = newSeatPosition;
        // Đặt ghế vào vị trí mới
        foreach (var cell in newCells)
        {
            cell.isOccupied = true;
            cell.objectOncell = seat;
        }

        // Cập nhật vị trí mới cho ghế
        seat.currentRow = newRow;
        seat.currentCol = newCol;

        return true;
    }


    // Hàm trả về danh sách các ô cần chiếm cho một ghế
    private List<GridCell> GetCellsForSeat(int startRow, int startCol, Seat seat)
    {
        // Tùy thuộc vào loại ghế và hướng ghế, trả về các ô cần chiếm
        //List<GridCell> cells = new List<GridCell>();
        List<GridCell> cells = new();
        switch (seat)
        {
            case SingleSeat:
                cells.Add(gridCells[startRow, startCol]);
                break;

            case DoubleSeat doubleSeat:
                switch (doubleSeat.direction)
                {
                    case Direction.Right:
                        if (startRow + 1 < rows) //rows lớn hơn row index 1 đơn vị
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow + 1, startCol]);
                        }
                        else
                        {
                            Debug.Log("ghế loại 2 bị vượt xuống dưới grid");
                        }
                        break;
                    case Direction.Left:
                        if (startRow - 1 >= 0)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow - 1, startCol]);
                        }
                        else
                        {
                            Debug.Log("ghế loại 2 bị vượt lên trên grid");
                        }
                        break;
                    case Direction.Up:
                        if (startCol + 1 < cols)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow, startCol + 1]); // Cột cộng 1
                        }
                        else
                        {
                            Debug.Log("ghế loại 2 bị vượt sang phải grid");
                        }
                        break;
                    case Direction.Down:
                        if (startCol - 1 >= 0)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow, startCol - 1]); // Cột trừ 1
                        }
                        else
                        {
                            Debug.Log("ghế loại 2 bị vượt sang trái grid");
                        }
                        break;
                }
                break;

            case TripleSeat tripleSeat:
                switch (tripleSeat.direction)
                {
                    case Direction.Right:
                        if (startRow + 2 < rows)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow + 1, startCol]);
                            cells.Add(gridCells[startRow + 2, startCol]);
                        }
                        else
                        {
                            Debug.Log("ghế loại 3 bị vượt xuống dưới grid");
                        }
                        break;
                    case Direction.Left:
                        if (startRow - 2 >= 0)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow - 1, startCol]);
                            cells.Add(gridCells[startRow - 2, startCol]);
                        }
                        else
                        {
                            Debug.Log("ghế loại 3 bị vượt lên trên grid");
                        }
                        break;
                    case Direction.Up:
                        if (startCol + 2 < cols)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow, startCol + 1]); // Cột cộng 1
                            cells.Add(gridCells[startRow, startCol + 2]); // Cột cộng 2
                        }
                        else
                        {
                            Debug.Log("ghế loại 3 bị vượt sang phải grid");
                        }
                        break;
                    case Direction.Down:
                        if (startCol -2 >= 0)
                        {
                            cells.Add(gridCells[startRow, startCol]);
                            cells.Add(gridCells[startRow, startCol - 1]); // Cột trừ 1
                            cells.Add(gridCells[startRow, startCol - 2]); // Cột trừ 2
                        }
                        else
                        {
                            Debug.Log("ghế loại 3 bị vượt sang trái grid");
                        }
                        break;
                }
                break;

            default:
                throw new ArgumentException("Unknown seat type");
        }
        return cells;
    }

    void ColorSeat(GameObject seatObject, Seat seat)
    {
        Renderer seatRenderer = seatObject.GetComponentInChildren<Renderer>();
        if (seatRenderer == null)
        {
            Debug.Log("không thấy component render để tạo màu cho ghế");
        }
        if (seatRenderer != null)
        {
            //Debug.Log("thêm màu");
            //Debug.Log(seat.seatColor);
            seatRenderer.material.color = ColorHelper.GetColor(seat.seatColor); // Thay đổi màu ghế
        }
    }

    public int GetGridRow()
    {
        return rows;
    }
    public int GetGridCol()
    {
        return cols;
    }
    public Vector3 GetThickTilePrefab()
    {
        return thickTilePrefab;
    }

    public int GetLevel()
    {
        string levelName = levelData.name;
        // Tách số level từ tên (ví dụ: "LevelData1" -> 1)
        string levelNumberString = System.Text.RegularExpressions.Regex.Match(levelName, @"\d+$").Value;

        // Chuyển đổi sang số nguyên
        if (int.TryParse(levelNumberString, out int levelNumber))
        {
            return levelNumber; // Trả về số level
        }
        else
        {
            Debug.LogWarning($"Could not parse level number from name: {levelName}");
            return -1; // Trả về -1 nếu không tách được số
        }
    }
}
