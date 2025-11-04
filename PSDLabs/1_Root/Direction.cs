namespace PSDLabs;

public struct Direction {
    public int x;
    public int y;

    public Direction(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public Direction Invert() {
        return new Direction(x * -1, y * -1);
    }

    public static bool operator ==(Direction a, Direction b) {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Direction a, Direction b) {
        return !(a == b);
    }
}